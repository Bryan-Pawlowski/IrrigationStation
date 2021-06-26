# Practice ProtoBuf
___
[![Gitter](https://badges.gitter.im/IrrigationStation/Practice.svg)](https://gitter.im/IrrigationStation/Practice?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)

## Wait whaaa....?

Ok so yeah we're going to put some practice stuff in here because I'm a huge noob and I'm not afraid to show it. Yee.
So I'm thinking we could do something really fun but also super lame-o. We're going to make a parking lot data layout
where we've got cars, vans, etc.

## What's to come

I want to create a couple of dummy projects in here. The first manages the cars going into and coming out of the 
lot. We want another application to look at this data and output the status of the parking lot. I am not doing this
because it is ***good design***. I think there are a lot of arguments as to how actually
garbage a deployable solution like this would be, but that's not what this is for. This is for the purposes of my 
embarrassingly and voluntarily public learning endeavors. I don't pretend to know all things. I am simply inviting y'all
along as I try to learn more things.

For reals though I super promise to write about what's up with that proto file.

## Dat Proto

Before we can begin with any development over on this end, we first need to make sure our protobuf library/toolkit is 
installed. To do this go to the [Protobuf](https://github.com/PixelChaserB/IrrigationStation/tree/main/ProtoBuf) page 
follow the instructions in the section titled "Protobuf setup for Windows."

## ImGui.NET

Originally, this example was going to be spread across a C++ and a C# implementation which would read in/out the same protobuf 
files to communicate with each other. At this point in my life/energy level, I feel pretty comfortable with _not_ taking that strategy 
just creating a really awesome GUI version of it in C# instead. I have been using Dear IMGUI a whole lot for my real life job, so I 
figured I'd give myself a break and use it for this example project.

## Lot Manager

This is a silly example project to demonstrate the power/ease-of-use of the protobuf system in a hopefully-digestible way 
such that I can grok it enough to use it effectively for the actual irrigation project.

Our silly example projects will be implementing a fake parking lot management system for a single machine. The "Lot Manager" could be
conceptualized as a system only accessible to a parking lot employee who would be responsible for tracking which vehicles are
parked where, if there are any.

## Lot Viewer

The lot viewer can be understood to be a display for a parking lot customer. Nobody wants this, nobody asked for this. I am not 
doing this because I think it would be a good idea. I am doing it because it's what I could think up when trying to come up with 
some nontrivial example of how I intend to use Protobuf in the full project.

This section will be filled once I come up with a plan for this.

## Need Help?

Click that chat badge at the top of the readme!