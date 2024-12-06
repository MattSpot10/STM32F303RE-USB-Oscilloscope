### **Overview**
This project includes hardware, documentation, and other components needed for an oscilloscope build around an STM32F303RE microcontroller.
It is build upon, and inspired by, the [EMBO (EMBedded Oscilloscope)](https://github.com/parezj/EMBO) project by Jakub Pařez

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
