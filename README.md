# WhatIsTheWeather
A weather app that sources data from the OpenWeatherMap Current Weather API.

## Contents
1. Installation
2. Usage
    - Data Files
    - Components
    - Site Logic
    - User Interface
    - Report Output
3. References
4. Contributors

## 1. Installation
Clone this repository into Visual Studio using the "Clone Repository" option in the "Git" menu.

## 2. Usage
### Data Files
The app depends on 5 JSON files for proper functionality:
- **a2isocodes.json**: List of ISO 3166-1 Alpha-2 codes for all countries.
- **omwappid.json**: OpenWeatherMap Current Weather API application ID.
- **unitconv.json**: Unit conversion factors for measurements.
- **units.json**: Unit strings for measurements.
- **usstates.json**: List of US states and their abbreviations.

All files are components for the site logic with the exception of omwappid.json, which is used in the API call. The site form will render only if every data file is properly retrieved through HTTP and deserialized. The files **a2isocodes.json** and **usstates.json** are used to generate selectors in the form on the user interface.

The **unitconv.json** and **units.json** files are used for mapping the data received from the API into units more commonly used in weather forecasts. This also performs conversions on values not converted to the proper unit format by the API (e.g. precipitation in **mm** to **in** for imperial).

### Components
Two classes are defined to assist in handling the websites input and output:
- **Forecast**: Forecast data encapsulator capable of accepting deserialized json responses from the OpenWeatherMap Current Weather API.
- **FormData**: Input form component that stores user's desired query arguments for the OpenWeatherMap Current Weather API.

The **Forecast** class definition, in addition to providing a property-based interface for interacting with deserialized data, provides a number of methods to expand the usefulness of the data:
- **GetTime()**: Converts unix time property, dt + timezone, to DateTime component.
- **GetSunrise()**: Converts unix time property for sunrise time to DateTime component.
- **GetSunset()**: Converts unix time property for sunset time to DateTime component.
- **WindCardinalDirection()**: Maps the wind direction property (in degrees) to cardinal direction based on a 16-point compass rose.
- **WeatherPrimary()**: Macro for accessing the first element in the weather property. If the data was properly deserialized into an object of the Forecast class, then this data will always be good.

The **Forecast** class definition also provides an interface to two nested support classes, **WeatherDesc** and **Sys**, which act as encapsulators for dictionary items in the JSON API response that contain data of different types.

The **FormData** class definition provides an interface for the UI form fields to bind to, annotating required fields and constraining the input length of the City/Zipcode text input field.

### Site Logic
The first thing the site attempts to do is load all 5 input files from the "data" directory. If this fails, the input form in not rendered and no furhter actions can be taken. Otherwise, the form renders, ready for user interaction.
The user must specify a country and city/zipcode to request weather data for. An optional "choose state" field is rendered if the user selects the United States as the country (OpenWeatherMap supports searching by state only for the U.S.).
This field is only used if the user specified a city rather than a zipcode. The user can also choose between metric and imperial units. The user is notified of any incomplete or incorrect (input text field too long) input.

Upon the user submission, the form checks for valid data. If the data is valid, an API call is made querying by zipcode and country. If the first call does not work, a second one is made, this time querying by city name and country (city, state, and country in the case of the US).
If both calls are bad, an exception catch is logged to the console. Otherwise, the response is deserialized into a **Forecast** object and the data is rendered to the page above the user input form.

### User Interface
The initial form rendered to the user's screen contains three fields:
- **Country selection**
- **City/Zipcode text field (max string length of 128 characters)**
- **Unit type selection (defaulting to metric)**

A fourth **State selection** field is rendered below the **Country selection** field if the Unitied States is chosen in the latter. The user may choose to use this to narrow their search or ignore it.
Once the user fills out the required parts of the form and abides by their constraints (the original three), the user may submit the form to make a query.

### Report Output
Upon a successful API call, a report is rendered to the user's screen containing the weather forecast for the day. The elements of the form include (rendered top to bottom, left to right):
- **Weather group icon**
- **Weather group description**
- **Current temperature**
- **Temperature maximum/minimum**
- **Temperature "feels like"**
- **Wind speed with \*wind gust speed**
- **Visibility**
- **Barometric pressure**
- **Humidity**
- **Cloud cover**
- **Sunrise time**
- **Sunset time**
- **\*Rainfall over the past hour**
- **\*Rainfall over the past three hours**
- **\*Snowfall over the past hour**
- **\*Snowfall over the past three hours**
- **Latitude**
- **Longitude**
- **Time weather was updated by the API**
- **Link to information source**

\* Element is only rendered if the API responded with measurements for that element.

## 3. References

- **CSS Styling**. Bootstrap 4: https://getbootstrap.com/docs/4.0/getting-started/introduction/
- **ISO 3166-1 Alpha-2 Codes JSON** Available at: https://github.com/lukes/ISO-3166-Countries-with-Regional-Codes/blob/master/slim-2/slim-2.json
- **US State Abbreviation JSON**. Available at: https://gist.github.com/mshafrir/2646763#file-states_titlecase-json

## 4. Contributors
**Author**. Kyler Dickey
