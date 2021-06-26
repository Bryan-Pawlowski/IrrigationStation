#include <iostream>
#include <App/Reader.h>
#include <fstream>

using namespace std;

inline bool FileExists(const string& name)
{
    struct stat buffer;
    return( stat(name.c_str(), &buffer) == 0);
}

int main(int argc, char** argv)
{

    ///check arg count first.
    if(argc != 2)
    {
        cout << "[ERROR]Incorrect number of arguments!" << endl;
        cout << "Usage: Reader.exe <filepath>" << endl;
        cout << "No other usage exists." << endl;
        return 1;
    }

    const string fname(argv[1]);

    Practice::ParkingLot* Lot = new Practice::ParkingLot();

    ///Check if file exists
    if(FileExists(fname))
    {
        ///Do parsing
        fstream input(fname, ios::in | ios::binary);
        Lot->ParseFromIstream(&input);

        ///Do Display
        if(Lot->IsInitialized())
        {
            Reader MyReader(Lot);
            cout << MyReader.DisplayLot();
            return 0;
        }
    }
    else
    {
        cout << "[ERROR]File at path '" << argv[1] << "' does not exist!";
        return 1;
    }



    return 0;
}
