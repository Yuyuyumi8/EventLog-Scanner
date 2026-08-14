# SharpEventLog

**SharpEventLog** is a lightweight C# console application that scans and analyzes specific Event IDs from the **Windows Security Event Log**.
It primarily processes **4624 (Logon Success)** and **4625 (Logon Failure)** events to gather information about logon attempts.
The application extracts useful fields such as IP address, username, and timestamp, then displays them in a clean and readable format.

## Features

* Reads records from the Windows Security Event Log
* Filters by specific Event IDs (4624 and 4625 included by default)
* Extracts username and remote IP address from event messages
* Cleans unnecessary whitespace
* Displays detailed output for Logon Success / Logon Failure events
* Simple, clear, and easily extendable code structure

## Which Event IDs Are Processed?

By default:

| Event ID | Description      |
| -------- | ---------------- |
| **4624** | Successful logon |
| **4625** | Failed logon     |

You can use the `ProcessLogEntries` method to read logs with different Event ID values if needed.

## Requirements

* Windows operating system (Security event log required)
* .NET Framework / .NET (depending on the version used)
* Running as Administrator is recommended (needed for Security log access)


## Compilation

- Open the *SharpEventLog.sln* file.

- **Build the solution**

```
Build → Build Solution   (Ctrl + Shift + B)
   ```

- After the build completes successfully, the executable file will be located at:

```
SharpEventLog\bin\Debug\SharpEventLog.exe
   ```


## Usage

When you run the application, it automatically scans for Event IDs 4624 and 4625:

```bash
SharpEventLog.exe
```

Example output:

```
========== SharpEventLog -> Event ID 4624 (Success) ==========

-----------------------------------
Time: 01.12.2029 14:22:01
Status: Success
Username: Administrator
Remote IP: 10.0.0.15
```

## License

This project is licensed under the MIT License. For more information, see the [LICENSE file](LICENSE).