#pragma once
#include <string>
using namespace std;

class Program04
{
public:
	Program04();
	~Program04();
	typedef struct Link
	{
		string Addr;
		int data;
		string nextAddr;
		Link* next;
	}* pLink;
	void Start();
private:
	string start;
	int len;
	int position;
	pLink head;
	pLink FindElement(pLink, string);
	pLink FindPositionElement(pLink, int);
	pLink FindLastElement(pLink);
};

