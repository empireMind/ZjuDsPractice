#include "Program04.h"
#include <iostream>
#include <fstream>
#include <string>
using namespace std;

Program04::Program04()
{
	ifstream fsIn("文件\\Program04.txt");
	if (!fsIn.is_open())
	{
		cout << "未成功打开文件" << endl;
	}
	fsIn >> start >> len >> position;
	pLink last;
	for (int i = 0; i < len; i++)
	{
		pLink temp = new Link();
		fsIn >> temp->Addr >> temp->data >> temp->nextAddr;
		if (i == 0)
			head = temp;
		else
			last->next = temp;
		if (i == len - 1)
			temp->next = head;
		last = temp;
	}
	//cin >> start >> len >> position;
	//pLink* linkArr = new pLink[len];
	//pLink last;
	//for (int i = 0; i < len; i++)
	//{
	//	pLink temp = new Link();
	//	cin >> temp->Addr >> temp->data >> temp->nextAddr;	
	//	if (i == 0)
	//		head = temp;
	//	else
	//		last->next = temp;
	//	if (i == len - 1)
	//		temp->next = head;
	//	last = temp;
	//}
}

Program04::~Program04()
{

}

void Program04::Start()
{	
	if (len == 1)
	{
		cout << head->Addr << " " << head->data << " " << head->nextAddr << endl;
		return;
	}

	int time = len / position;
	if (time*position < len)
		time += 1;

	pLink* arr = new pLink[time];
	pLink startP = FindElement(head, start);
	for (int i = 0; i < time; i++)
	{
		pLink revP;
		if (i > 0)
		{
			startP = FindElement(head, arr[i-1]->Addr);
			startP = FindElement(startP, startP->nextAddr);
		}
		bool inverse;
		revP = FindPositionElement(startP, position, inverse);
		pLink curr = CreateLink(revP);
		arr[i] = curr;
		for (int j = position; j > 1; j--)
		{
			pLink temp;
			if (curr->nextAddr == "-1" && !inverse)
			{
				curr->next = nullptr;
				break;
			}			
			if (inverse)
				temp = FindLastElement(curr);
			else
				temp = FindElement(head, curr->nextAddr);
			curr->next = CreateLink(temp);
			curr->nextAddr = curr->next->Addr;
			curr = curr->next;
			if (j == 2)
				curr->next = nullptr;			
		}
	}
	if (position == 1)
	{
		for (int i = 0; i < time; i++)
		{
			arr[i]->next = nullptr;
		}
	}

	pLink res = CreateLink(arr[0]);
	pLink tail = res;
	for (int i = 0; i < time; i++)
	{
		while (tail->next != nullptr)
		{
			tail = tail->next;
		}
		if (i + 1 < time)
		{
			tail->next = arr[i + 1];
			tail->nextAddr = arr[i + 1]->Addr;
		}
	}
	tail->nextAddr = "-1";
	PrintLink(res);
}

Program04::pLink Program04::CreateLink(pLink src)
{
	pLink ret = new Link();
	ret->Addr = src->Addr;
	ret->data = src->data;
	ret->nextAddr = src->nextAddr;
	ret->next = src->next;
	return ret;
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

Program04::pLink Program04::FindPositionElement(pLink link, int N, bool& inverse)
{
	pLink temp = link;
	for (int i = 1; i < N; i++)
	{
		if (temp->nextAddr == "-1")
		{
			inverse = false;
			return link;
		}
		else
			temp = FindElement(temp, temp->nextAddr);
	}
	inverse = true;
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

void Program04::PrintLink(pLink link)
{
	while (link != nullptr)
	{
		cout << link->Addr << " " << link->data << " " << link->nextAddr << endl;
		link = link->next;
	}
}