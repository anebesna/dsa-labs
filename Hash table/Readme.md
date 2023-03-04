Task: сollision resolution method: double hashing. Develop an online
platform user database.

Key: string username.

Values: string password, string emailAddress, DatelastLoginDate. The
password must be stored in a hash table in hashed form; a separate
function must be developed for this hashing passwords.

The custom Date structure contains the following fields: int year,
string month, int day. The value of the last date authorization can be
set manually when adding a new one user or be generated pseudorandomly.
In addition to the usual search, an additional one is implemented an
authorization in the system that is checked to see if it exists a user
with the given name and password in the database (name and password are
entered by the user from the console). If such a user does not exist or
if it was entered wrong password, the corresponding one is displayed
message. The program contains an additional function
accountDeactivation(string username) which when trying authorization
checks when the last time the user went toundefined systems. If the last
authorization was at least 60 days ago, the user is removed from the
database data, and the corresponding output is displayed to the user
information message. In the user menu should be provided the possibility
of authorization on the online platform.

The Hashtable class describes a hash table. It contains fields:

table -- an array that represents a hash table;

loadness -- the load of the table, i.e. the number of non-empty table
slots;

size -- the number of keys contained in the table.

The Key class describes a key. Contains fields according to the option.

The Value class describes the value of a key. Contains fields according
to the option.

The Entry class describes an element to be added to the hash table.
Contains fields:

key -- Key type key,

value -- value of the Value type.
