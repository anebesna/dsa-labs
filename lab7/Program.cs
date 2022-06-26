using System;
using static System.Console;

namespace lab7
{
    class Program
    {
        static int capacity = 11;
        static HashTable table = new HashTable();
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Write("\nEnter the command or press /help to see the available commands: ");
                    string command = ReadLine();
                    switch (command)
                    {
                        case "/help":
                            Write("/help - help\n1 - create new hashtable\n2 - insert entry\n3 - remove entry\n" +
                                "4 - find entry\n5 - show current table\n6 - control example\n7 - log in\n8 - exit");
                            break;

                        case "1":
                            table = new HashTable();
                            break;

                        case "2":
                            {
                                Write("Enter username: "); string name = ReadLine();
                                var e = table.findEntry(new Key(name));
                                while (e.key.username == name || e.key.username == "DELETED")
                                {
                                    Write("Such user already exists or was deleted! Try another username: ");
                                    name = ReadLine();
                                    e = table.findEntry(new Key(name));
                                }
                                Write("\nEnter password: "); string password = ReadLine();
                                Write("\nEnter email: "); string email = ReadLine();
                                try
                                {
                                    Write("\n"+@"Enter last login date as ""day month year"" or /rand: ");
                                    Date date;
                                    string[] temp;
                                    int day, year;
                                    string month;
                                    string s = ReadLine();
                                    if (s == "/rand")
                                    {
                                        day = new Random().Next(1, 29);
                                        month = Enum.Parse(typeof(Date.Months), new Random().Next(1, 12).ToString()).ToString();
                                        year = new Random().Next(2018, 2022);
                                        date = new Date(day, month, year);
                                    }
                                    else
                                    {
                                        temp = s.Split(" ");
                                        date = new Date(int.Parse(temp[0]), temp[1], int.Parse(temp[2]));
                                    }
                                    e = new Entry(new Key(name), new Value(password, email, date));
                                    table.insertEntry(e.key, e.value);
                                    Write("User was successfully inserted!");
                                }
                                catch { Write("Entered data was in invalid format."); }
                                break;
                            }

                        case "3":
                            {
                                Write("Enter username: "); string name = ReadLine();
                                try
                                {
                                    table.removeEntry(new Key(name));
                                    Write("User was successfully removed!");
                                }
                                catch { Write("Such user doesn't exist"); }
                                break;
                            }

                        case "4":
                            {
                                Write("Enter username: "); string name = ReadLine();
                                var e = table.findEntry(new Key(name));
                                if(e.key.username == null ) { WriteLine("Such user doesn't exist"); }
                                else if(e.key.username == "DELETED") { WriteLine("This user was deleted"); }
                                else Write($"Username: {e.key.username}\tPassword: {e.value.password}\tEmail: {e.value.emailAddress}\tLast login date: {e.value.lastLoginDate.day} {e.value.lastLoginDate.month} {e.value.lastLoginDate.year}");
                                break;
                            }

                        case "5":
                            table.show();
                            break;

                        case "6":
                            Example();
                            break;

                        case "7":
                                table.Login();
                                break;

                        case "8":
                            Environment.Exit(0);
                            break;

                        default:
                            WriteLine("Wrong command");
                            break;
                    }
                }
                catch
                {
                    WriteLine("Error");
                }
            }
        }

        public static void Example()
        {
            table = new HashTable();
            WriteLine("Let's add 5 users to the database.");
            table.insertEntry(new Key("anna"), new Value("qwerty", "anna@gmail.com", new Date(12, "March", 2022)));
            table.insertEntry(new Key("sofia"), new Value("sofia", "sofia@gmail.com", new Date(19, "June", 2022)));
            table.insertEntry(new Key("andrew"), new Value("andrew", "andrii@gmail.com", new Date(13, "November", 2021)));
            table.insertEntry(new Key("dave"), new Value("wolikesh", "dave@gmail.com", new Date(09, "September", 2021)));
            table.insertEntry(new Key("luke"), new Value("skywalker", "luke@gmail.com", new Date(28, "May", 2022)));
            WriteLine("Now let's check the table:");
            table.show();
            WriteLine("Let's add two more users to call rehashing.");
            table.insertEntry(new Key("adam"), new Value("sandler", "adam@gmail.com", new Date(24, "February", 2022)));
            table.insertEntry(new Key("eve"), new Value("medrano", "eve@gmail.com", new Date(21, "February", 2022)));
            WriteLine("Now let's check the table:");
            table.show();
            WriteLine("Let's remove the user adam:");
            table.removeEntry(new Key("adam"));
            table.show();
            WriteLine("Let's find anna:");
            var e = table.findEntry(new Key("anna"));
            WriteLine($"Username: {e.key.username}\tPassword: {e.value.password}\tEmail: {e.value.emailAddress}\tLast login date: {e.value.lastLoginDate.day} {e.value.lastLoginDate.month} {e.value.lastLoginDate.year}");
            WriteLine("Let's try to deactivate dave:");
            table.accountDeactivation("dave");
        }

        class HashTable
        {
            public Entry[] table;
            public double loadness;
            public int size;
            public HashTable()
            {
                table = new Entry[capacity];
                loadness = 0;
                size = 0;
            }

            public void insertEntry(Key key, Value value)
            {
                if (loadness >= 0.5)
                {
                    WriteLine("\nLoadness is too big! Rehashing...\n");
                    rehashing();
                }
                Entry entry = new Entry(key, value);
                entry.value.password = hashPassword(entry.value.password).ToString();
                int h1 = firstHash(key, table.Length);
                int h2 = secondHash(key, table.Length);
                int i = h1;
                for (int k = 1; table[i].key.username != null && table[i].key.username != key.username; k++)
                {

                    i = (h1 + k * h2) % table.Length;
                }
                if (table[i].key.username == null)
                {
                    size++;
                    loadness = Math.Round((double)size / table.Length, 2);
                }
                table[i] = entry;
            }

            public void removeEntry(Key key)
            {
                int h1 = firstHash(key, table.Length);
                int h2 = secondHash(key, table.Length);
                int hash = h1;
                for (int k = 1; table[hash].key.username != null && table[hash].key.username != key.username; k++)
                {
                    hash = (h1 + k * h2) % table.Length;
                }
                if (table[hash].key.username != null)
                {
                    table[hash] = new Entry(new Key("DELETED"), new Value(null, null, new Date()));
                    size--;
                    loadness = (double)size / table.Length;
                    WriteLine("Entry has been deleted!");
                }
                else WriteLine("Entry has not been deleted! Try again.");
            }

            public Entry findEntry(Key key)
            {
                int h1 = firstHash(key, table.Length);
                int h2 = secondHash(key, table.Length);
                int hash = h1;
                for (int k = 1; table[hash].key.username != null && table[hash].key.username != "DELETED" && table[hash].key.username != key.username; k++)
                {
                    hash = (h1 + k * h2) % table.Length;
                }
                return table[hash];
            }

            public int firstHash(Key key, int i)
            {
                return hashCode(key) % i;
            }

            public int hashCode(Key key)
            {
                int res = 0;
                for (int i = 0; i < key.username.Length; i++)
                {
                    res += 37 * (3 * i + 1) + key.username[i];
                }
                return res;
            }

            public int secondHash(Key key, int i)
            {
                return i - 1 - (hashCode(key) % (i - 1));
            }

            public int hashPassword(string password)
            {
                int res = 0;
                for (int i = 0; i < password.Length; i++)
                {
                    res += 37 * (2 * i + 3) + password[i];
                }
                return res;
            }

            public void rehashing()
            {
                int n = table.Length;
                int m = n * 2;

                Entry[] newTable = new Entry[m];

                for (int i = 0; i < n; i++)
                {
                    if (table[i].key.username == null || table[i].key.username == "DELETED") continue;
                    int h1 = firstHash(table[i].key, m);
                    int h2 = secondHash(table[i].key, m);
                    int k = 1;
                    int index = h1;
                    while (newTable[index].key.username != null)
                    {
                        index = (h1 + k * h2) % m;
                        k++;
                    }
                    newTable[index] = table[i];
                }

                table = newTable;
                loadness = (double)size / m;
            }

            public void show()
            {
                WriteLine("Current table:");
                foreach (Entry e in table)
                {
                    WriteLine($"Username: {e.key.username, 8}\tPassword: {e.value.password, 8}\tEmail: {e.value.emailAddress, 15}\tLast login date: {e.value.lastLoginDate.day} {e.value.lastLoginDate.month} {e.value.lastLoginDate.year}");
                }
            }
            public void Login()
            {
                Write("Enter username: "); string name = ReadLine();
                Write("\nEnter password: "); string password = ReadLine();
                var e = findEntry(new Key(name));
                if (e.key.username == null) { WriteLine("Such user doesn't exist"); }
                else if (e.key.username == "DELETED") { WriteLine("This user was deleted"); }
                else if (!accountDeactivation(name))
                {
                    if (e.value.password == hashPassword(password).ToString())
                    {
                        WriteLine("You entered the system!");
                        Write($"Username: {e.key.username}\tPassword: {e.value.password}\tEmail: {e.value.emailAddress}\tLast login date: {e.value.lastLoginDate.day} {e.value.lastLoginDate.month} {e.value.lastLoginDate.year}");
                    }
                    else WriteLine("Wrong password. Access denied.");
                }
            }
            
            public bool accountDeactivation(string username)
            {
                bool deactivated = false;
                Entry e = findEntry(new Key(username));
                int day = e.value.lastLoginDate.day;
                int month = (int)Enum.Parse(typeof(Date.Months), e.value.lastLoginDate.month);
                int year = e.value.lastLoginDate.year;
                DateTime today = DateTime.Parse(DateTime.Today.ToString("d"));
                DateTime date = DateTime.Parse($"{month}/{day}/{year}");
                if((today-date).TotalDays >= 60)
                {
                    removeEntry(new Key(username));
                    Write("User has been deleted for not being active for more than 60 days.");
                    deactivated = true;
                }
                return deactivated;
            }
        }
        struct Entry
        {
            public Key key;
            public Value value;
            public Entry(Key key, Value value)
            {
                this.key = key;
                this.value = value;
            }
        }
        struct Key
        {
            public string username;
            public Key(string username)
            {
                this.username = username;
            }
        }
        struct Value
        {
            public string password, emailAddress;
            public Date lastLoginDate;
            public Value(string password, string emailAddress, Date lastLoginDate)
            {
                this.password = password;
                this.emailAddress = emailAddress;
                this.lastLoginDate = lastLoginDate;
            }

        }
        struct Date
        {
            public int day, year;
            public string month;
            public Date(int day, string month, int year)
            {
                this.day = day;
                this.month = month;
                this.year = year;
            }
            public enum Months
            {
                January = 1,
                February,
                March,
                April,
                May,
                June,
                July,
                August,
                September,
                October,
                November,
                December
            }
        }
    }       
}
