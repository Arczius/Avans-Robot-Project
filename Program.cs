using Avans.StatisticalRobot;

bool ledBlink = false;
bool ignorePress = false;

Led led = new Led(5);
Button button = new Button(6);

int intervalMs = 330;

long GetCurrentMs(){
    return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
}

long lastMs = GetCurrentMs();

// play a startup sound
Robot.PlayNotes("l8 >d>e>f>g>a>b>c");

while(true) 
{
    if(button.Pressed() && !ignorePress){
        if(ledBlink){
            led.SetOff();
        }

        ledBlink = !ledBlink;
        ignorePress = !ignorePress;
    }
    else if(!button.Pressed() && ignorePress){
        ignorePress = !ignorePress;
    }

    if(ledBlink){
        Robot.Motors(125, -125);
    }
    else{

        Robot.MotorsOff();
    }


    if(ledBlink && GetCurrentMs() > (lastMs + intervalMs)){
        Console.WriteLine("Het aantal millivolts :" + Robot.ReadBatteryMillivolts());
        lastMs = GetCurrentMs();

        if(led.GetStatus() == "on"){
            led.SetOff();
            Robot.LEDs(0,0,255);
        }
        else{
            led.SetOn();

            Robot.LEDs(255,0,0);
        }
    }
}
