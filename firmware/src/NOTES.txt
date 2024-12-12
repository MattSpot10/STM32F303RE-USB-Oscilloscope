Both the primary and secondary firmware are loaded into the STM32 Flash at different locations. The primary at 0x08000000 and the secondary
at 0x08010000. The primary (containing oscillscope firmware) jumps to the secondary firmware (Mass storage device containing download links)
if the PC12 pin is set.

The binaryies in the firmware root folder have been combined to include both the primary and secondary firmware in the same hex file for
easy programming of the chip.