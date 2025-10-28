# AntsTown 
##### - algorithm-data project of Mr.Khademi in 1403/term1

AntsTown is a small C# console application that demonstrates string matching, grouping, and Huffman encoding/decoding of generated "ants" (strings). The program reads a `parents.txt` file (queens and males), finds child substrings using KMP, groups them by length (radix-like), builds Huffman codes from character frequencies, then provides an interactive menu to Encode (produce `order.bin`) or Decode (`order.bin` → console).

---

## Requirements

- .NET Framework 4.7.2
- Visual Studio 2022 (or another IDE that supports .NET Framework)

---

## parents.txt

Place a `parents.txt` file in the project root (or one of the relative search paths the program prints). The program searches several relative paths; it prints each path as it checks.

File format:
- First line: two integers separated by space — `<number_of_queens> <number_of_males>`
- Next N lines: queen strings (one per line)
- Next M lines: male strings (one per line)

Example:
```
2 2 
abc
bcde
abbb
abcd
```
---

## How it works (high level)

1. Program finds and reads `parents.txt`.
2. For every queen × male pair it runs KMP (pattern = queen, text = male) to extract child substrings.
3. Child substrings are grouped by length (Radix sort-like into fixed buckets).
4. Character frequencies (including a newline separator `\n` after each child) are counted and a Huffman tree is built to produce binary codes.
5. Menu:
   - Encode (1): prompts you to enter a queue (list of ants). It searches that queue inside the generated children list, encodes the matched sequence into bits using Huffman codes, writes `order.bin` to the current working directory.
   - Decode (2): reads `order.bin`, converts bytes to bits and decodes back to characters using the Huffman codes generated from the loaded `parents.txt`.
   - Exit (3)

---

## Files of interest

- `Program.cs` — entry point, file search, orchestrates pipeline.
- `Classes.cs` — `Kmp`, `Radix` helper algorithms.
- `halfman.cs` — Huffman tree builder and related classes.
- `Encoder.cs` — queue input, encoding flow, `order.bin` writer.
- `Decoder.cs` — reads `order.bin`, bit/byte conversion and decoding.
- `BackaUP.cs` — older commented-out backup of Huffman logic.

---

## Output

- `order.bin` — binary file containing encoded bitstream (created in the program working directory). Decoder attempts to find it in several relative paths and prints the path when found.
