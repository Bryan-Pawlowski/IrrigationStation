# IrrigationStation

## About

Welcome to Irrigation Station! This is Bryan's vision to actually get 
some sort of real and fun side project going that actually has something
to do with what I went to University for. Strange times. 

I wanted to create a project that could be helpful for people to learn how
to contribute to a small yet nontrivially-sized project spanning multiple
disciplines. Before we get into the different branches of the project,
let's talk vision.

## Vision

I have so many Raspberry Pis lying around and there are [so many dang
guides](https://bc-robotics.com/tutorials/raspberry-pi-irrigation-control-part-1-2/) 
and tutorials out there on how to get the Raspberry Pi irrigation
system going, that I thought it'd be great to extend these projects 
by creating some tools to go along with them. We'll need to add
some data gathering/serialization into the irrigation control software (**Python**). If 
our irrigation controller is recording/broadcasting data, it'd be cool to do 
something with that data (**C++**?) and also create a nice little control app (**C#**) 
for our irrigation system. For this project, we will be leveraging Google's 
[**Protocol Buffers**](https://developers.google.com/protocol-buffers) 
to make the data serialization/parsing side a bit easier on ourselves.

## Project Folders

### IrrigationController

This side will likely be in python. It will handle deciding to apply water to a 
container if certain measurement conditions are met. The measurements and subsequent 
conditions will ultimately be similar to what was defined in that bc-robotics guide 
(linked above). I'll try to leave some room in the code design to support running 
different types of calculations/measurements to A/B test.

### RemoteControl

This will be a windows desktop app, implemented in c#. I know that this introduces 
limitations to who might be able to utilize our remote control app, but this is my 
project and freakin' dangit this is what I wanna do.

### BonkersVisualizer

Take that data, put it where it doesn't belong (C++) and do some bonkers graphics 
visualizations on that junk that probably won't make *any* sense!

### WebPortal

I want to do this. I think it makes sense to run from the raspberry pi itself, 
so I'm thinkin' python with some sort of HTML hosting. This is honestly a blind spot 
for me. I'll do some research on what our options are.

### ProtoBuf

This is where our data message definitions will go. That definition (.proto) file will 
be compiled into C#, C++ and Python generated code, so getting our data into a code-able
state will be a nothing task. Woohoo!

## Can I Use This?

Yes.