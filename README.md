# Mars Program Simulator

A program that simulates mars mission and outputs the final position and direction of each rover and its status.

## How to run `MarsProgram.exe`

### Requirements

- IDE capablity to compile and run .net 9.0

### Steps

1. Clone the repository.
2. Open the solution file `MarsProgram.sln` in your IDE.
3. Build and run the executable `MarsProgram.exe` with input arguments as described below.

Alternatively, you can run the program by writing additional unit tests in the [Simulation Unit Tests file](MarsProgram.UnitTests/SimulationTest.cs).

### args

#### `-input <value>`

String input containing the boundaries of the mars and individual rover commands.

#### input format

```
5  3           // top right boundary of mars
1  1  E        // starting position and direction of first rover
RFRFRFRF       // command sequence for first rover

3  2  N        // starting position and direction of second rover
FRRFLLFFRRFLL  // command sequence for second rover

0  3  W        // starting position and direction of third rover
LLFFFLFLFL     // command sequence for third rover
```

#### output format

```
1  1  E             // final position and direction of first rover
3  3  N  LOST       // final position and direction of second rover and status (Status is only shown if Lost)
2  3  S             // final position and direction of third rover
```
