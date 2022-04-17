Sean Sutton (16 April 2022)
	
*** IMPORTANT! ***
If the incorrect API Key is entered the first time this app is run, you will need to MANUALLY DELETE the settings.txt file in the app directory and rerun the application.
	
	Notes:
	- Expected to run out of the box. .NET Framework 4.7.1 and upwards.
    - Newtonsoft JSON library uses MIT license.

    Assumptions:
    - Valid expiry date is always found at index zero in MOT JSON array.
    - Further sample registrations and 'quirks' will be forthcoming ahead of roll-out.
    - API Key upload to Source Control is absolutely not allowed.

    Improvements:
    - Parse all MOT Array items, hash expiry dates, then look for latest date? (in case array order ever changes).
    - More readable Expiry Date.
    - Add -help option re-iterating that API Key may need be incorrect.
    - Unit testing and all-round testing: cache of known registrations and MOT-related facts.
    - Dev Logs.
    - Remove obj folder from Source Control.
    - Better API Key user-entry handling and management.


*** IMPORTANT! ***
If the incorrect API Key is entered the first time this app is run, you will need to MANUALLY DELETE