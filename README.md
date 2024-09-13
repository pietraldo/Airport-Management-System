<div align="center">

 <img src="https://github.com/pietraldo/Airport-Management-System/blob/main/Zrzut%20ekranu%202024-09-13%20182230.png" width="60%" />

# Airport Management System

A Airport Management System developed incrementally for simulating real time flights and managing them. 
Key functionalities include reading and serializing data from FTR files, simulating real-time TCP data streams, snapshot creation, object updates, and data filtering. 
The code is designed for easy extension and maintainability with a focus on modularity.

</div>


## ⚡️ Quick Start
1. Clone the repository.
2. Open the .sln file in Visual Studio.
3. Build and run the project.

## ✏️ Commands
Use the following commands in the console:

`print` – Creates a snapshot of the entire dataset and saves it to a file.<br>
`report` – Displays the data in the console.<br>
`exit` – Exits the application.<br>

## 📝 Custom SQL for Data Management
Manage data using SQL-like commands:

`display` – Shows information in a table.<br>
Syntax: `display {object_fields} from {object_class} [where {conditions}]`<br><br>

`update` – Updates data.<br>
Syntax: `update {object_class} set ({key_value_list}) [where {conditions}]`<br><br>

`delete` – Deletes data.<br>
Syntax: `delete {object_class} [where {conditions}]`<br><br>

`add` – Adds a new object.<br>
Syntax: `add {object_class} new ({key_value_list})`<br><br>

## 📄 Logging
All data changes are logged, including updates, ID changes, and failed operations. Example logs:
```text
Id: 1329, ID changed from 499 to 1329
Operation UpdateId on object 1475 failed
Id: 1073, Position changed from (157.99564, 3.403886, 11982) to (-145.21829, -87.924355, 292.1136)
```
## 💡 Additional Information
The application supports data from both files and a network simulator. The network simulator also sends real-time data change events.
