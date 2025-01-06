

### **Purchasing Components**

**Printed Circuit Board & Components**
- PCB [JLCPCB](https://jlcpcb.com/), [STM32F303 Oscilloscope Gerbers](https://github.com/MattSpot10/STM32F303RE-USB-Oscilloscope/blob/main/hardware/pcb%20v1.1/Gerber_Stm32f303-Oscilloscope-V1.1.0_PCB_Stm32f303-Oscilloscope_2025-01-04.zip) (Use your favorite PCB fab. I use JLCPCB and upload the Gerber files. Use 1.6mm board thickness)

- PCB components, [LCSC BOM tool](https://www.lcsc.com/bom), [Oscilloscope Bill of Materials](https://github.com/MattSpot10/STM32F303RE-USB-Oscilloscope/blob/main/hardware/pcb%20v1.1/bill%20of%20materials/(Filled)%20BOM_Stm32f303-Oscilloscope-V1.1.0_2025-01-04.csv) (Use LCSC's Bill of Materials upload tool and upload this project's BOM.)

- Order additonal capacitors for tuning. 0.5pF, 0.75pF, 1pF, 1.3pF are good extra 0402 capacitors to purchase along with the project's BOM in order to ensure proper tuning. This is used to tune the capacitive resistive divider located in the 1Meg ohm resistor divider.

**3D Printed Case**
- Black PLA
- [screws (M2x20 and M2x4)](https://www.aliexpress.us/item/3256805681030772.html?spm=a2g0o.order_list.order_list_main.16.3d791802mM3GtA&gatewayAdapt=glo2usa)

**Programming**
- [St-link v2](https://www.amazon.com/ACEIRMC-Emulator-Downloader-Programmer-Programming/dp/B0D3D3BPXK/ref=sr_1_16?crid=32OYTKYL8YATC&dib=eyJ2IjoiMSJ9.S2bx_Fvl83DMyyARd8TBfs0AuRfp00ljUaiQA3bpaU_CvClry9UNY-ow_zON9vXPwRR2cw951_AZ8tIEDMAUF9B4C9bmIZtuhcNZ4sAUAgJ-AXCjxn4eXWm0MvYhUtBXyAGdeB8wRHiCZHmtsRKwB2MTt8Az4s5Bb2mTGOBloi51dHUZVGPpVM8cCGmkrdzsWPOKBU2r68Z_leSYcIDSjtm2XNihhsxhR4Bkud1gsf8.SBG86zCxN--AnBl5wd_xyVTlEYVj8TvY9tOvC_bWqGQ&dib_tag=se&keywords=st+link&qid=1736120060&sprefix=st+l%2Caps%2C649&sr=8-16)

**Additional Components**
- [Cheap oscilloscope probes](https://www.aliexpress.us/item/3256801670232474.html?spm=a2g0o.order_list.order_list_main.23.3d791802mM3GtA&gatewayAdapt=glo2usa)
- [Female BNC connectors (to make your own adapter cables)](https://www.aliexpress.us/item/3256806139723858.html?spm=a2g0o.order_list.order_list_main.38.3d791802mM3GtA&gatewayAdapt=glo2usa)
- [Male PCB BNC connectors from Aliexpress](https://www.aliexpress.us/item/2251832707392796.html?spm=a2g0o.order_list.order_list_main.28.7aa41802EOVonR&gatewayAdapt=glo2usa)
  
<br>

### **Assembling PCB**
- Use the projects schematics and PCB to help you place all of the componets. [Homepage Link](https://oshwlab.com/mspotten/stm32f303-occiliscope-new), [Editor Link](https://easyeda.com/editor#project_id=62f6949196fe46afb045491ae7696041)
- When soldering the STM32F303 MCU ensure to get it's orintation correct. pin 1, with the marking, is the bottom left corner of the chip in reference to the text.
- Cut off the extra length of pins when you've completed soldering the THT components (this ensures that the PCB will fit in the case).

### **Downloading firmware**
- Connect your St-link to the programming pins (5V, SWDIO, SWCLK, and GND) of the Oscilloscope. This is the header next to the USB-B connector.
- Download the [STM32CubeProgrammer software](https://www.st.com/en/development-tools/stm32cubeprog.html) and well as [our binary](https://github.com/MattSpot10/STM32F303RE-USB-Oscilloscope/blob/main/firmware/firmware%20v1.0.1.0.bin)
- Download the binary to the Oscilloscope and you should be ready to dissconnect your st-link and connect to the computer through the USB-B connector. (Remember the board has an on-board power switch on the right side of the USB connector).
- The slide switch on the left of the USB-B connector will determine the Boot Mode. If the switch is switched to the left it will act as a Mass Storage Device and allow you to download the software and find information about the project. If this switch is switched to the right it will act as an oscilloscope.

### **Tuneing Oscilloscope inputs**
Place your scope on the 1x setting and probe a square wave. You can use the PWM output for this. Make sure to set it to the AC mode so the signal is not cut off on the top of the range (measures -3.3v to 3.3v) If the signal does not look square (rising edge is higher/falling edge is lower or signal rises and falls slowly) it means that your DC gain does not match your AC gain. To fix this try different values of capacitors on the bottom side of the resistor capacitor divider next to the Oscillscope inputs (these values should be in the range of 0pF to 2pF if the top capacitor is 56pF).

### **Printing PCB Case and Assembly**
[bottom.stl](https://github.com/MattSpot10/STM32F303RE-USB-Oscilloscope/blob/main/hardware/case/case%20bottom.stl)

[top.stl](https://github.com/MattSpot10/STM32F303RE-USB-Oscilloscope/blob/main/hardware/case/case%20top.stl)

I chose these settings in Cura for slicing:
```
Layer Height: .2mm
Wall Line Count: 4
Top Layers: 7
Bottom Layers: 7
Infill Density: 20% cubic
```

The M2x4 screws are used to secure the PCB to the bottom of the case. The M2x20 screws are used to secure the bottom and the top of the case together.
### **Adding light pipe**
The acryilic rods can be cut to the desired length and inserted into the holes to allow the light from the LEDs to transfer effectivly (I used hot glue to secure them).

### **Adding sticker**
A sticker can be added to the top of the case to define functions. I printed mine on photopaper, laminated it and cut out the white squares to reveal the switches headers and LEDs. I secured the sticker with double sided tape.
[sticker.jpg](https://github.com/MattSpot10/STM32F303RE-USB-Oscilloscope/blob/main/hardware/case/top%20sticker.jpg)

