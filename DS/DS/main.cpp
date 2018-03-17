#include<iostream>
#include"Program01.h"
#include"Program02.h"

using namespace std;

int main()
{
	Program02* p = new Program02();
	p->Start();
	while (true)
	{
		if (getchar() == 'r')
			break;
	}
	return 0;
}