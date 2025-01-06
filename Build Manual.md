
---

### **Purchasing Components**

**Printed Circuit Board & Components**
- PCB: [JLCPCB](https://jlcpcb.com/), [STM32F303 Oscilloscope Gerbers](https://github.com/MattSpot10/STM32F303RE-USB-Oscilloscope/blob/main/hardware/pcb%20v1.1/Gerber_Stm32f303-Oscilloscope-V1.1.0_PCB_Stm32f303-Oscilloscope_2025-01-04.zip)  
  (Use your preferred PCB fab; I use JLCPCB. Upload the Gerber files and select 1.6mm board thickness.)

- PCB components: [LCSC BOM tool](https://www.lcsc.com/bom), [Oscilloscope Bill of Materials](https://github.com/MattSpot10/STM32F303RE-USB-Oscilloscope/blob/main/hardware/pcb%20v1.1/bill%20of%20materials/(Filled)%20BOM_Stm32f303-Oscilloscope-V1.1.0_2025-01-04.csv)  
  (Use LCSC’s Bill of Materials upload tool and upload this project's BOM.)

- Additional capacitors for tuning: 0.5pF, 0.75pF, 1pF, and 1.3pF 0402 capacitors are useful for proper tuning. These capacitors are part of the capacitive-resistive divider located in the 1MΩ resistor divider.

**3D Printed Case**
- Material: Black PLA
- [Screws (M2x20 and M2x4)](https://www.aliexpress.us/item/3256805681030772.html?spm=a2g0o.order_list.order_list_main.16.3d791802mM3GtA&gatewayAdapt=glo2usa)

**Programming**
- [ST-Link V2](https://www.amazon.com/ACEIRMC-Emulator-Downloader-Programmer-Programming/dp/B0D3D3BPXK/ref=sr_1_16?crid=32OYTKYL8YATC&dib=eyJ2IjoiMSJ9.S2bx_Fvl83DMyyARd8TBfs0AuRfp00ljUaiQA3bpaU_CvClry9UNY-ow_zON9vXPwRR2cw951_AZ8tIEDMAUF9B4C9bmIZtuhcNZ4sAUAgJ-AXCjxn4eXWm0MvYhUtBXyAGdeB8wRHiCZHmtsRKwB2MTt8Az4s5Bb2mTGOBloi51dHUZVGPpVM8cCGmkrdzsWPOKBU2r68Z_leSYcIDSjtm2XNihhsxhR4Bkud1gsf8.SBG86zCxN--AnBl5wd_xyVTlEYVj8TvY9tOvC_bWqGQ&dib_tag=se&keywords=st+link&qid=1736120060&sprefix=st+l%2Caps%2C649&sr=8-16)

**Additional Components**
- [Acrylic Rods for LED light pipes on case](https://www.amazon.com/gp/product/B0B4JRXKHL/ref=ppx_yo_dt_b_search_asin_title?ie=UTF8&th=1)
- [Cheap Oscilloscope Probes](https://www.aliexpress.us/item/3256801670232474.html?spm=a2g0o.order_list.order_list_main.23.3d791802mM3GtA&gatewayAdapt=glo2usa)
- [Female BNC Connectors (for making your own adapter cables)](https://www.aliexpress.us/item/3256806139723858.html?spm=a2g0o.order_list.order_list_main.38.3d791802mM3GtA&gatewayAdapt=glo2usa)
- [Male PCB BNC Connectors from Aliexpress](https://www.aliexpress.us/item/2251832707392796.html?spm=a2g0o.order_list.order_list_main.28.7aa41802EOVonR&gatewayAdapt=glo2usa)

---

### **Assembling PCB**

- Use the project’s schematics and PCB layout to help you place all components.  
  [Homepage Link](https://oshwlab.com/mspotten/stm32f303-occiliscope-new)  
  [Editor Link](https://easyeda.com/editor#project_id=62f6949196fe46afb045491ae7696041)

- When soldering the STM32F303 MCU, ensure correct orientation. Pin 1, with it's marking, is the bottom left corner of the chip in reference to the text.

- After soldering the THT components, cut off the extra length of the pins to ensure the PCB fits into the case.

---

### **Downloading Firmware**

1. Connect your ST-Link to the programming pins (5V, SWDIO, SWCLK, and GND) on the Oscilloscope. These are located next to the USB-B connector.

2. Download the [STM32CubeProgrammer software](https://www.st.com/en/development-tools/stm32cubeprog.html) and the [firmware binary](https://github.com/MattSpot10/STM32F303RE-USB-Oscilloscope/blob/main/firmware/firmware%20v1.0.1.0.bin).

3. Use STM32CubeProgrammer to upload the firmware to the Oscilloscope. After programming, disconnect your ST-Link and connect to the computer via the USB-B connector.  
   (Remember, the board has an on-board power switch located to the right of the USB connector.)

4. The slide switch to the left of the USB-B connector determines the Boot Mode:  
   - If the switch is to the left, the device will act as a Mass Storage Device, providing you links to install software and project recources
   - If the switch is to the right, the device will act as an oscilloscope.

---

### **Tuning Oscilloscope Inputs**

- Set your oscilloscope to the 1x setting and probe a square wave. You can use the PWM output for this test.  
- Ensure you use the AC coupling mode so that the signal does not get clipped (measuring between -3.3V to 3.3V).

- If the signal does not look square (e.g., rising edges are higher/falling edges are lower or the signal rises and falls slowly), this indicates a mismatch in your DC gain and AC gain.

- To fix this, try different capacitor values (ranging from 0pF to 2pF) on the bottom side of the resistor-capacitor divider next to the oscilloscope inputs. The top capacitor by default is 56pF

---

### **Printing PCB Case and Assembly**

- [case bottom.stl](https://github.com/MattSpot10/STM32F303RE-USB-Oscilloscope/blob/main/hardware/case/case%20bottom.stl)
- [case top.stl](https://github.com/MattSpot10/STM32F303RE-USB-Oscilloscope/blob/main/hardware/case/case%20top.stl)

I used to following slicer settings in Cura:
```
Layer Height: .2mm
Wall Line Count: 4
Top Layers: 7
Bottom Layers: 7
Infill Density: 20% cubic
```
- The M2x4 screws are used to secure the PCB to the bottom of the case. 
- The M2x20 screws are used to secure the bottom and the top of the case together.

---

### **Adding Light Pipe**

- Cut acrylic rods to the desired length and insert them into the holes to transfer light from the LEDs effectively.  
  (I used hot glue to secure them.)

---

### **Adding Sticker**

- A sticker can be added to the top of the case to define functions.  
  I printed mine on photo paper, laminated it, and cut out the white squares to reveal the switches, headers, and LEDs. I secured the sticker with double-sided tape.  
  [Sticker Image](https://github.com/MattSpot10/STM32F303RE-USB-Oscilloscope/blob/main/hardware/case/top%20sticker.jpg)

