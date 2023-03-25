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

## Getting Started

TBD

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

## Contributing

Contributions to this project are welcome. If you find any issues or have suggestions for improvements, feel free to open an issue or submit a pull request.


## License

This project is licensed under the MIT License - see the `LICENSE` file for details.
