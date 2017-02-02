# NetTimeZoneAPI
C# Class for obtain a new DateTime Object from a location (Latitude and Longitude)

# How to use
  Copy / Add the class to your own proyect
# How to send push
  First you need have a token from your proyect in Google api Javascript
# Example
	1) Create a Object "TimeZoneAPI" with your token proyect ( Google api Javascript):
```c#
TimeZoneAPI tzp = new TimeZoneAPI("token_here");
```
	2) Call the method GetDateTime:
```c#
DateTime dt = tzp.GetDateTime(new TimeZoneParam()
{
    location = "24.79032,-107.38782", //Latitude and Longitude in string whitout spaces,
    timestamp = TimeZoneAPI.DateTimeToUnixTimestamp() 
   // DateTimeToUnixTimestamp get the DateTime.UtcNow in timespan format but you can use pass a DateTime
   // DateTimeToUnixTimestamp(DateTime dt) 
});
```
