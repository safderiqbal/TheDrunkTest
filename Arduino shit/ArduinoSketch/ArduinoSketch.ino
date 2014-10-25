/*
  Uses https://github.com/Seeed-Studio/Grove_LED_Bar for the LED bar.
*/
#include <LED_Bar.h>
#define analogInDatPin 0

const int buttonPin = 6;

int sensorValue = 0;
int readyForRead = 0;
int buttonState = 0;
int maxReading = 0;
int currentReading = 0;
int startedRead = 0;
int lastLevel = 0;

LED_Bar bar(9, 8);

void setup()
{
    Serial.begin(9600);                         // open the serial port at 9600 bps
}

void loop()
{
  
  //Wait for a byte to be sent down from the server or already received one and ready for read.
  if(Serial.available() > 0)
  {
    if (Serial.read() > 0)
    {
      readyForRead = 1;
      Serial.println("Hello...Is there anybody out there");
    }
  }
  
  buttonState = digitalRead(buttonPin);
  
  if(readyForRead == 1 && buttonState == 0)
  {
    flickerLeds();
  }  
  
  if(readyForRead == 1 && buttonState == 1)
  {
      currentReading = analogRead(analogInDatPin);
      
      if(currentReading > maxReading){
        maxReading = currentReading;
      }
      
      startedRead = 1;
  }
  
  if(buttonState == 0 && startedRead == 1)
  {
    Serial.println(currentReading);
    startedRead = 0;
    maxReading = 0;
    currentReading = 0;
    readyForRead = 0;
  }
}

void flickerLeds()
{
  if(lastLevel == 0){
     bar.setLevel(10); 
     lastLevel = 10;
  }
  else{
    bar.setLevel(0);
    lastLevel = 0;
  }
  delay(1000);
}
