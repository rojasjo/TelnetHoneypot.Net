# TelnetHoneypot.Net

This is a simple telnet honeypot written in C# that can be used to monitor and log telnet connection attempts.

## Disclaimer

**WARNING:** This code is provided as-is with no warranty or guarantee of any kind. By using this software, you assume all responsibility for any damage
 that may occur to your system. Use at your own risk.

## Security Considerations

Running a honeypot can potentially expose your system to attackers, so it's important to take appropriate precautions to ensure your system is not compromised. Here are some recommended measures:

* Least privilage principle: run it as a non-privileged user to limit its access to system resources
* Isolation: Run the honeypot in a docker container, virtual machine or on a dedicated system that contains no valuable data.
* Network segmentation: Deploy the honeypot on a separate network segment or VLAN to isolate it from other systems on your network.
* Monitoring: monitor the honeypot logs regularly for any suspicious activity.

## Build

### Prerequisites
Having the .net 7 SDK installed to build the executable

### Build process

1. clone the repository

`git clone https://github.com/rojasjo/TelnetHoneypot.Net.git`

2. move to repository root folder

`cd TelnetHoneypot.Net`

3. build the solution with the dotnet CLI, eg:

```dotnet publish -r linux-x64 -p:PublishSingleFile=true --self-contained false```

The program would be located in the publish folder:

`..../TelnetHoneypot.Net/TelnetHoneypot/bin/Debug/{dotnet-sdk-version}/{built-platform}/publish`

For more info https://learn.microsoft.com/en-us/dotnet/core/deploying/single-file/overview?tabs=cli

4. move the file from the publish folder to your desired one
5. run the program

`./TelnetHoneypot`

## Usage

After executing the program, it starts listening for incoming connections on port 23. Additionally, a folder named `Logs` is created in the same directory as the executable file. You can monitor the contents of this folder to gain insights into any attacker activity.

### Authentication

To emulate the Telnet connection, the user is prompted to authenticate.

#### Valid logins

| Username  | Password |
| ------------- |:-------------:|
| admin      | admin     |

### Log

The log is formated as `yyyy-MM-dd HH:mm:ss.fff#LOGTYPE message`  example `2023-03-25 10:22:03.512#COMMAND: quit`

## Commands

The following commands are supported by the honeypot:

* __help__: Displays a list of available commands.
* __pwd__: Prints the current working directory.
* __dir__: Lists files in the current directory.
* __cd__ <directory>: Changes the current directory.
* __date__: Displays the current date and time.
* __whoami__: Displays the current username.
* __system__: Displays system information.
* __ping__ <host>: Pings a specified host.

## CVEs

**TBD**

## Contributing

Contributions to this project are welcome. If you find any issues or have suggestions for improvements, feel free to open an issue or submit a pull request.

## License

This project is licensed under the MIT License - see the `LICENSE` file for details.