.   My code with very simple way, take the instruction from the text box to get in function, do the calculations, and show the results of calculations in data memory grid view and pipeline registers grid view. 
 With the details of the code, and how is organized; I take the instruction from the text box and split it into two parts; the first part is the pc counter, that automatically    
Starts with 1000, we take it, and go to the Fetch stage; in fetch sage, we put the pc counter in instruction memory, then we add to it 4, and go to the decode stage.
In decode stage, we take the second part of instruction, and split it to get the opcode (the first part) to know if the instruction is R-type or I-type, and handle the instruction with the suitable way, and fill the register memory, and we move to the third stage (execute stage).
In this stage, we take the data from the decode stage, and handle it like: R-type or I-type; If it is R-type, we will see the function code to specify the type of function, and calculate the data depends on the type of it, else it will handle it like I-type, which has the sign extension, add the sign extension to the first input of the data,
And move the result and the destination register to the memory access stage.
This one is a simple stage; we take the result and see if the instruction is R-type, we don’t read or write anything in memory, else we read the data from memory;
And we move to the last stage.
 Write back stage, at last we get the result of instruction and write it in register memory in the destination register.
 
  I organize my code like: the first part I initialize the array of mips register, and 
The data memory in initialize button, and write the component like: data memory, alu, register memory, and take this component to use in stages of the instruction processes in the cycle-1 button
