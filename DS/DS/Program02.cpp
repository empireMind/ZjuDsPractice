#include "Program02.h"
#include <iostream>
using namespace std;

Program02::Program02()
{
	cout << "Set Array Length:" << endl;
	cin >> len;
	cout << "Input Intergers in Array" << endl;
	arr = new int[len];
	for (int i = 0; i < len; i++)
	{
		cin >> arr[i];
	}
}

Program02::~Program02()
{
}

void Program02::Start()
{
	for (int i = 0; i < len; i++)
	{
		if (arr[i] >= 0)
			break;
		if (i == len - 1)
		{
			cout << 0 << " " << arr[0] << " " << arr[len-1] << endl;
			return;
		}
	}

	RetStruct* ret = DivideAndConquer(arr, 0, len - 1);
	cout << ret->res << " " << arr[ret->left] << " " << arr[ret->right] << endl;
}

Program02::RetStruct* Program02::DivideAndConquer(int* list, int left, int right)
{
	RetStruct *maxLeftRet, *maxRightRet;

	if (left == right)
	{
		//if (list[left] > 0)
			return new RetStruct{ list[left], left, right };
		//else
			//return new RetStruct{ 0, left, right};
	}

	int center = (left + right) / 2;
	maxLeftRet = DivideAndConquer(list, left, center);
	maxRightRet = DivideAndConquer(list, center + 1, right);

	int maxLeftBorderSum(0), maxRightBorderSum(0), leftBorderSum(0), rightBorderSum(0);
	RetStruct *leftBorderRet = new RetStruct(), *rightBorderRet=new RetStruct();
	
	for (int i = center; i >= left; i--)
	{		
		leftBorderSum += list[i];
		if (i == center)
		{
			maxLeftBorderSum = leftBorderSum;
			if (list[i] != 0)
			{
				leftBorderRet->res = leftBorderSum;
				leftBorderRet->left = i;
				leftBorderRet->right = center;
			}
		}				
		if (leftBorderSum > maxLeftBorderSum)
		{
			maxLeftBorderSum = leftBorderSum;
			leftBorderRet->res = leftBorderSum;
			leftBorderRet->left = i;
			leftBorderRet->right = center;
		}
	}
	for (int i = center + 1; i <= right; i++)
	{
		rightBorderSum += list[i];
		if (i == center + 1)
		{
			maxRightBorderSum = rightBorderSum;
			if (list[i] != 0)
			{
				rightBorderRet->res = rightBorderSum;
				rightBorderRet->left = center + 1;
				rightBorderRet->right = i;
			}
		}
		if (rightBorderSum > maxRightBorderSum)
		{
			maxRightBorderSum = rightBorderSum;
			rightBorderRet->res = rightBorderSum;
			rightBorderRet->left = center + 1;
			rightBorderRet->right = i;
		}
	}

	RetStruct* maxBorderRet = new RetStruct();
	maxBorderRet->res = leftBorderRet->res + rightBorderRet->res;
	//maxBorderRet->res = maxBorderRet->res < 0 ? 0 : maxBorderRet->res;
	maxBorderRet->left = leftBorderRet->left;
	maxBorderRet->right = rightBorderRet->right;

	return Max3(maxLeftRet, maxRightRet, maxBorderRet);
}

Program02::RetStruct* Program02::Max3(RetStruct* a, RetStruct* b, RetStruct* c)
{
	RetStruct* max;
	if (a->res > b->res)
		max = a;
	else if (a->res == b->res)
	{
		if (a->left < b->left)
			max = a;
		else if (a->left == b->left)
		{
			if (a->right <= b->right)
				max = a;
			else
				max = b;
		}
		else
			max = b;
	}
	else
		max = b;

	if (max->res > c->res)
		return max;
	else if (max->res == c->res)
	{
		if (max->left < c->left)
			return max;
		else if (max->left == c->left)
		{
			if (max->right <= c->right)
				return max;
			else
				return c;
		}
		else
			return c;
	}
	else
		return c;
}
