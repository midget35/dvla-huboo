Sean Sutton (16 April 2022)
	
	Notes:
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
