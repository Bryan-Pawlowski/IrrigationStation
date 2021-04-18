//
// Created by Bryan on 4/17/2021.
//

#include "App/Reader.h"
#include <string>
#include <sstream>

using namespace std;

string Reader::DisplayLot()
{
    if(Initialized())
    {
        return GenerateOutput();
    }
    else
    {
        stringstream errstream(""); ///<- lol sounds like "airstream"
        errstream << "Reader's data was not initialized!" << endl;
        errstream << "Double-check the file location you are trying to use ";
        errstream << "as it may not have a compatible data file, or the file ";
        errstream << "may not exist.";
        return errstream.str();
    }
}

string Reader::GenerateOutput()
{
    ///TODO: Generate the output.
    return TODO_MSG;
}

inline bool Reader::Initialized()
{
    bool Initialized = false;

    Initialized = MyLot != nullptr;

    return Initialized ? MyLot->IsInitialized() : false;
}