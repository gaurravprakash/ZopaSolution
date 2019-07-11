# Zopa homework

The console application can be obtained by building this solution. The application validates user input, and displays appropriate messages.
If user input is correct, it calculates and displays the result in the expected format.

# Packages
Using CsvHelper 12.1.1 for reading and parsing csv data.

# Future scope
The application design could further be modified to have interfaces for Calculator, Reader etc. Concrete implementations of these interfaces may then be injected by using some IoC container.

I have tried to keep the solution simple and straight forward. Please feel free to share your thoughts.

# How to run
1. Build the solution.
2. Open a command prompt/terminal window.
3. Navigate to debug/bin or release/bin folder as per the selected build configuration.
4. Execute command ZopaConsole.exe [market_file_path] [loan_amount] .
