#include "Program04.h"
#include <iostream>
#include <string>
using namespace std;

Program04::Program04()
{
	cin >> start >> len >> position;
	pLink* linkArr = new pLink[len];
	pLink last;
	for (int i = 0; i < len; i++)
	{
		pLink temp = new Link();
		cin >> temp->Addr >> temp->data >> temp->nextAddr;	
		if (i == 0)
			head = temp;
		else
			last->next = temp;
		if (i == len - 1)
			temp->next = head;
		last = temp;
	}
}

Program04::~Program04()
{

}

void Program04::Start()
{
	int time = len / position;
	if (time*position < len)
		time += 1;

	pLink* arr = new pLink[time];
	pLink curr = head;
	pLink startP = FindElement(curr, start);
	for (int i = 0; i < time; i++)
	{
		pLink revP;
		if(i>0)
			startP = FindElement(revP, revP->nextAddr);
		revP = FindPositionElement(startP, position);
		pLink temp = revP;
		for (int j = position; j > 1; j--)
		{
			temp->next = FindLastElement(temp);
			if(temp->next->Addr == arr[i-1]->Addr)
			{
				temp->next = nullptr;
				break;
			}
			temp = temp->next;
		}
		arr[i] = revP;
	}
}

Program04::pLink Program04::FindElement(pLink src, string value)
{
	pLink curr = src;

	while (curr->Addr != value)
	{
		curr = curr->next;
	}

	return curr;
}

Program04::pLink Program04::FindPositionElement(pLink link, int N)
{
	pLink temp = link;
	for (int i = 1; i < N; i++)
	{
		if (temp->nextAddr == "-1")
		{
			if (i < N - 1)
				return link;
			else
				temp = FindElement(temp, start);
		}
		else
			temp = FindElement(temp, temp->nextAddr);
	}
	return temp;
}

Program04::pLink Program04::FindLastElement(pLink src)
{
	pLink curr = src;

	while (curr->nextAddr != src->Addr)
	{
		curr = curr->next;
	}
	
	return curr;
}