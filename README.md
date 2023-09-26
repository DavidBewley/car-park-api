# car-park-api
## Pipeline
[![CI](https://github.com/DavidBewley/car-park-api/actions/workflows/pipeline.yml/badge.svg)](https://github.com/DavidBewley/car-park-api/actions/workflows/pipeline.yml)
## Endpoints

| HTTP Type | Endpoint  | Description |
|------------- |------------- | ------------- |
| GET | /api/availability  | Check the availability and price between 2 given dates  |
| POST | /api/booking  | Create a booking  |
| GET | /api/booking/{id}  | Get detailed information for a booking  |
| PUT | /api/booking/{id}  | Update a booking's dates  |
| DELETE | /api/booking/{id}  | Delete a booking  |

Booking body for POST and PUT:
```json
{
	"StartDate" : "2023-01-01",
	"EndDate" : "2023-01-10"
}
```

## SQL
A full list of tables and stored procedures requried for the api to function are present in the SQL folder. The release script can be ran on an empty database to create a working system.