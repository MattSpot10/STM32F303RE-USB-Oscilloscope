This folder contains files related to patching the original STM32F303 Oscilloscope firmware in Ghidra to support booting to the secondary
firmware on the condition of PC12 being set.

EMBO_F303RE_userFunction is the working one. The others are other implemetnaions that don't work and I don't feel like deleting yet.

It also contains a batch script to combine the two output Hex Files. The one from this folder and the one from the secondary firmware
folder ouput by STM32CubeIDE. (I have not tried it yet, as at this point I got my binary files from reading off the STM32 flash).