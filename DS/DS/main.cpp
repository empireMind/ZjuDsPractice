#include<iostream>
//#include"Program01.h"
//#include"Program02.h"
#include"Program04.h"

using namespace std;

int main()
{
	Program04* p = new Program04();
	p->Start();
	while (true)
	{
		if (getchar() == 'r')
			break;
	}
	return 0;
}