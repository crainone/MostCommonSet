# MostCommonSet
First attempt using proper C# documentation.

The actual behavior of this code is to print the most common set of characters. Consider a random sequence of characters to be a "word". 
These words should be composed of characters a-z in some order. There should be the same number of characters per word. Currently, this
is expected to be 5. These words should be printed one per line in the input file (which for now must be called testinput.txt in the
working directory). For example,

'''
xuspo
nxrkb
micxz
ysquw
psvkr
ujeiq
'''

You can generate random test data using GenerateData.exe, which is included in the project. It currently generates 10000 entries to a file
called "testinput.txt" in the working directory.

If there are multiple sets of characters that are tied, it shows all of them. If there is no data, the most common set of characters is
"". If there is one "word", that is automatically the most common word. The order of the characters does not matter.
