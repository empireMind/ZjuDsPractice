#pragma once

class Program03
{
public:
	Program03();
	~Program03();
	typedef struct Link
	{
		int factor;
		int expo;
		Link* next;
	}*pLink;

private:
	int lenA;
	int lenB;
	pLink linkHeadA;
	pLink linkCurrA;
	pLink linkHeadB;
	pLink linkCurrB;
	void InitialLink(int&, pLink&, pLink&);
	void LinkAdd(pLink, pLink, pLink&);
};


