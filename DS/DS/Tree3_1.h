#pragma once
#define MAXTREE 10
#define EMPTY -1
class Tree3_1
{
public:
	Tree3_1();
	~Tree3_1();
	struct BinTree
	{
		char data;
		int left;
		int right;
	}Tree1[MAXTREE], Tree2[MAXTREE];
private:
	int CreateTree(struct BinTree[]);
	int CreateTree_TxtTest(struct BinTree[]);
	bool IsSame(BinTree t1, BinTree t2);
	bool checkArr[MAXTREE];
};

