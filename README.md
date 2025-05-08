# Numerology API

A RESTful API built with ASP.NET Core for performing numerological calculations and analysis, including Birth Number calculations, Destiny Number calculations, mobile number analysis, and Vedic Grid interpretations.

## 🔢 Features

- **Birth Number (BN) Calculation**: Calculate a person's Birth Number from their date of birth
- **Destiny Number (DN) Calculation**: Calculate a person's Destiny Number from their full date of birth
- **Mobile Number Analysis**: 
  - Calculate the numerological total of a mobile number
  - Identify lucky and unlucky numbers based on BN and DN
  - Count occurrences of each digit
  - Detect friendly number combinations
  - Identify the most powerful number in the mobile number
  - Detect L-shape patterns
- **Vedic Grid Analysis**:
  - Generate a 3x3 Vedic Grid based on the mobile number
  - Identify significant number combinations
  - Provide pattern interpretations for detected formations

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core
- **Language**: C# (.NET)
- **Documentation**: Swagger/OpenAPI
- **CORS**: Configured for a Vite React frontend running on localhost:5173

## 🚀 Getting Started

### Prerequisites

- .NET 6.0 SDK or later
- Visual Studio 2022, VS Code, or any preferred IDE with C# support

### Installation

1. Clone this repository:
   ```
   git clone https://github.com/hk77921/NumerologyApi.git
   cd NumerologyApi
   ```

2. Build the solution:
   ```
   dotnet build
   ```

3. Run the application:
   ```
   dotnet run
   ```

4. The API will be available at:
   - API Endpoints: `http://localhost:5000/api/numerology`
   - Swagger Documentation: `http://localhost:5000/swagger`

## 📚 API Documentation

### Analyze Numerology

**Endpoint**: `POST /api/numerology/analyze`

**Request Body**:
```json
{
  "dob": "yyyy-MM-dd",
  "mobile": "1234567890"
}
```

**Response**:
```json
{
  "bn": 5,
  "dn": 7,
  "mobileTotal": 45,
  "luckyNumbers": [1, 3, 5],
  "unluckyNumbers": [9],
  "digitCounts": {
    "1": 1,
    "2": 1,
    "3": 1,
    "4": 1,
    "5": 1,
    "6": 1,
    "7": 1,
    "8": 1,
    "9": 1,
    "0": 1
  },
  "detectedCombinations": [
    [1, 5],
    [2, 7]
  ],
  "lShapeDetected": true,
  "powerfulNumber": 5
}
```

### Get Vedic Grid Analysis

**Endpoint**: `GET /api/numerology/vedic-grid?mobile=1234567890`

**Response**:
```json
{
  "grid": [
    [1, 1, 1],
    [1, 1, 1],
    [1, 1, 1]
  ],
  "combinations": ["3-1", "1-9", "..."],
  "analysis": ["3-1", "1-9", "..."],
  "patternInterpretations": [
    "Plane 3-1-9: Educated, respected, professionally involved",
    "..."
  ]
}
```

## 📦 Project Structure

```
NumerologyApi/
├── Controllers/
│   └── NumerologyController.cs
├── Models/
│   ├── NumerologyRequest.cs
│   ├── NumerologyResult.cs
│   └── VedicGridResponse.cs
├── Services/
│   ├── INumerologyService.cs
│   └── NumerologyService.cs
├── Utils/
│   └── NumerologyCalculator.cs
├── Program.cs
└── appsettings.json
```

## 🧮 Numerology Logic

### Birth Number (BN)
The Birth Number is calculated from the day of birth (reduced to a single digit).

### Destiny Number (DN)
The Destiny Number is calculated by adding all digits in the full date of birth and reducing to a single digit.

### Vedic Grid
A 3x3 grid arranged as follows:
```
3 1 9
6 7 5
2 8 4
```

### L-Shape Patterns
The API detects predefined L-shaped patterns in mobile numbers, such as:
- 3-1-9-5
- 6-7-5-8
- 2-8-4-9

### Friendly Pairs
Detected friendly number combinations include:
- 1-5
- 2-7
- 3-6
- 5-9
- 1-6
- 2-8

## 🔗 Frontend Integration

This API is designed to be used with a frontend application. CORS is configured to allow requests from `http://localhost:5173` (default Vite React app port).

## 📄 License

[MIT License](LICENSE)

## 👤 Author

- [hk77921](https://github.com/hk77921)
