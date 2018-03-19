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
	DeleteLink(linkHeadA);
	DeleteLink(linkHeadB);
}

void Program03::Start()
{
	pLink linkMultiplyRes;
	LinkMultiply(linkHeadA, linkHeadB, linkMultiplyRes);
	PrintLink(linkMultiplyRes);
	DeleteLink(linkMultiplyRes);
	pLink linkAddRes;
	LinkAdd(linkHeadA, linkHeadB, linkAddRes);	
	PrintLink(linkAddRes);
	DeleteLink(linkAddRes);
}

void Program03::PrintLink(pLink link)
{
	if (link->next == nullptr)
		cout << link->factor << " " << link->expo;
	while (link->next != nullptr)
	{
		link = link->next;
		cout << link->factor << " " << link->expo;
		if (link->next != nullptr)
			cout << " ";
	}
	cout << endl;
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
	pLink currA = linkA->next;
	pLink currB = linkB->next;
	pLink currRes = linkRes;
	while (currA != nullptr || currB != nullptr)
	{
		if (currA == nullptr)
		{
			while (currB != nullptr)
			{
				pLink temp = new Link();
				temp->expo = currB->expo;
				temp->factor = currB->factor;
				currRes->next = temp;
				currB = currB->next;
				currRes = currRes->next;
			}
			break;
		}
		if (currB == nullptr)
		{
			while (currA != nullptr)
			{
				pLink temp = new Link();
				temp->expo = currA->expo;
				temp->factor = currA->factor;
				currRes->next = temp;
				currA = currA->next;
				currRes = currRes->next;
			}
			break;
		}

		if (currA->expo > currB->expo)
		{
			pLink temp = new Link();
			temp->expo = currA->expo;
			temp->factor = currA->factor;
			currRes->next = temp;
			currA = currA->next;
			currRes = currRes->next;
		}
		else if (currA->expo < currB->expo)
		{
			pLink temp = new Link();
			temp->expo = currB->expo;
			temp->factor = currB->factor;
			currRes->next = temp;
			currB = currB->next;
			currRes = currRes->next;
		}
		else
		{
			pLink temp = new Link();
			temp->expo = currA->expo;
			temp->factor = currA->factor + currB->factor;
			if (temp->factor != 0)
			{
				currRes->next = temp;
				currRes = currRes->next;
			}
			currA = currA->next;
			currB = currB->next;
		}
	}
}

void Program03::LinkMultiply(pLink linkA, pLink linkB, pLink& linkRes)
{
	pLink currA = linkA->next;
	pLink currB = linkB->next;
	if (currA == nullptr || currB == nullptr)
	{
		linkRes = new Link();
		return;
	}
	pLink* linkArr = new pLink[lenA];
	for (int i = 0; i < lenA; i++)
	{
		pLink currLinkArr = (linkArr[i] = new Link());
		while (currB != nullptr)
		{
			pLink temp = new Link();
			temp->expo = currA->expo + currB->expo;
			temp->factor = currA->factor * currB->factor;
			currLinkArr->next = temp;
			currLinkArr = currLinkArr->next;
			currB = currB->next;
		}
		currA = currA->next;
		currB = linkB->next;
	}
	pLink temp = new Link();
	pLink res;
	for (int i = 0; i < lenA; i++)
	{
		LinkAdd(temp, linkArr[i], res);
		DeleteLink(temp);		
		DeleteLink(linkArr[i]);
		temp = res;
	}	
	delete[] linkArr;
	linkRes = res;
}

void Program03::DeleteLink(pLink link)
{
	pLink curr = link;
	pLink temp;
	do
	{
		temp = curr->next;
		delete curr;
		curr = temp;
	} while (temp != nullptr);		
}

