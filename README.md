### **Overview**
This project includes hardware, documentation, and other components needed for an oscilloscope build around an STM32F303RE microcontroller.
It is build upon, and inspired by, the [EMBO (EMBedded Oscilloscope)](https://github.com/parezj/EMBO) project by Jakub Pařez.

[Learn how to create your own!](google.com)

### **Hardware Features/Capabilities**

**Oscilloscope**
- 4 channels achieving up to 3.2MSps (max is 5MSps but is not stable).
- 44k samples (8bit) or 22k samples (12bit)
- 1 megaohm input impedance.
- 12pF input capacitance.
- BNC connector inputs - compatible with traditional oscilloscope probes.
- AC or DC coupled - configured by switch
- Configurable voltage input range by resistor divider. Default is -3.3V to 3.3V
- Overvoltage and undervoltage protection

**Signal Generator**
- 2 independent channels
- 4.5Mhz max
- selectable unipolar or bipolar output voltage mode (0V to 2.7V or -2.7V to 2.7V)
- BNC and 2.54mm header output

**Other instruments** - additional specifications can be found [here](https://github.com/parezj/EMBO)
- Logic Analyzer - 14.4MSps, 44kS, 2.54mm header input pins
- Voltmeter - 100Sps, same inputs as scope
- Counter - 57Mhz max, BNC input, undervoltage and overvoltage protection
- PWM Generator - 48Mhz max, BNC and 2.54mm header output

### **Firmware**
The firmware included in the project comprises of the [original firmware from the EMBedded Oscilloscope project](https://github.com/parezj/EMBO/releases) as well as a secondary firmware included in the same HEX file. The MCU either boots into the primary or secondary firmware depending on the state of the boot mode switch. The secondary firmware connects to the PC as a Mass Storage Device and includes links to install software and an optional autolaunch driver (autolaunch EMBO software on MCU USB connection).

### **Software/Drivers**
Included in this project is a version of the [original EMBO software](https://github.com/parezj/EMBO/releases) that has been edited to support pre-set and custom scope configurations, eliminating the need to manually set the offset and gain for the channel. When the 1x or 10x setting is selected, the gain and offset values are automatically adjusted accordingly.

An optional driver is available to autolaunch the EMBO Oscilloscope software each time the oscilloscope is connected to the PC


### **Documentation**
[Here is a link](google.com) to my github page where I explain the process of using these recources and files to create your own stm32 based oscilloscope. It's all there... from ordering the components and component selection, PCB, assembly, 3D printing case, sticker, firmware installation and more.

For a more in depth understaning of the project and the design process go ahead and check out [my youtube video](www.google.com). I go over the central challenges of the project and my reasoning behing my thinking.
