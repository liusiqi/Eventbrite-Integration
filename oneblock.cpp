#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
#include <vector>
#include <math.h>
using namespace std;

int main()
{
	for (int i = 0; i < 147; i++)
	{
		cout << i/50 + 1 << " : " << i%50 << endl;
	}
	return 0;
}