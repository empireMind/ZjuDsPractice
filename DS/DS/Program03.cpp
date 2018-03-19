#include "Program03.h"
#include <iostream>
using namespace std;

Program03::Program03()
{
	InitialLink(lenA, linkHeadA, linkCurrA);
	InitialLink(lenB, linkHeadB, linkCurrB);
}

Program03::~Program03()
{

}

void Program03::InitialLink(int& len, pLink& head, pLink& curr)
{
	cin >> len;
	head = new Link();
	curr = head;
	for (int i = 0; i < len; i++)
	{
		pLink element = new Link();
		int factor, expo;
		cin >> element->factor;
		cin >> element->expo;
		curr->next = element;
		curr = element;
	}
}

void Program03::LinkAdd(pLink linkA, pLink linkB, pLink& linkRes)
{
	linkRes = new Link();
	pLink currA = linkA;
	pLink currB = linkB;
	//pLink currA = linkA->next;
	//pLink currB = linkB->next;
	//if (currA->expo > currB->expo)
	//	linkRes->next = currA;
	//else if(currA->expo < currB->expo)
	//	linkRes->next = currB;
	//else
	//{
	//	pLink sum = new Link();
	//	sum->expo = currA->expo;
	//	sum->factor = currA->factor + currB->factor;
	//	linkRes->next = sum;
	//}
	while (currA->next != nullptr)
	{
		while (currB->next != nullptr)
		{

		}
	}
}

