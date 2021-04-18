//
// Created by Bryan on 4/17/2021.
//

#ifndef LOTVIEWERPROJECT_READER_H
#define LOTVIEWERPROJECT_READER_H
#include <Protobuf/Practice.pb.h>

///@brief Class to handle the reading functionality of our protocol buffer data.
///it will be constructed upon a pointer to a ParkingLot instance read-in from file.
class Reader
{

private:
    Practice::ParkingLot* MyLot;

    const std::string TODO_MSG = "TODO: Function not yet implemented.";

public:

#pragma region Constructors
    Reader(){ MyLot = nullptr; }
    Reader(Practice::ParkingLot* const & InLot)
    {
        MyLot = InLot;
    }
    Reader(Reader const & B)
    {
        MyLot = B.MyLot;
    }
#pragma endregion

    ///@brief Responsible for displaying a constructed string which lays out
    ///the parking lot status. This will first check that our data is in a readable
    ///state (checks if it's initialized) before attempting to generate the
    ///output string.
    std::string DisplayLot();

private:
    ///@brief Heavy-lifter for generating our parking lot output. Largely responsible for the whole thing.
    std::string GenerateOutput();

    ///@brief checks first if MyLot is not a null pointer.
    ///Then, it checks MyLot's IsInitialized()
    ///return value.
    ///@returns true if MyLot is not null and is initialized
    ///@returns false if MyLot is null
    ///@returns false if MyLot is not null but is not initialized
    inline bool Initialized();
};

#endif //LOTVIEWERPROJECT_READER_H
