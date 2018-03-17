#include "Program01.h"
#include <iostream>

using namespace std;

Program01::Program01()
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

Program01::~Program01()
{
}

void Program01::Start()
{
	int res = DivideAndConquer(arr, 0, len - 1);
	cout << "Max = " << res << endl;
}

int Program01::DivideAndConquer(int* list, int left, int right)
{
	int maxLeftSum, maxRightSum;

	if (left == right)
	{
		if (list[left] > 0)
			return list[left];
		else
			return 0;
	}

	int center = (left + right) / 2;
	maxLeftSum = DivideAndConquer(list, left, center);
	maxRightSum = DivideAndConquer(list, center + 1, right);

	int maxLeftBorderSum(0), maxRightBorderSum(0), leftBorderSum(0), rightBorderSum(0);
	for (int i = center; i >= left; i--)
	{
		leftBorderSum += list[i];
		if (leftBorderSum >= maxLeftBorderSum)
			maxLeftBorderSum = leftBorderSum;
	}
	for (int i = center + 1; i <= right; i++)
	{
		rightBorderSum += list[i];
		if (rightBorderSum >= maxRightBorderSum)
			maxRightBorderSum = rightBorderSum;
	}

	return Max3(maxLeftSum, maxRightSum, maxLeftBorderSum+maxRightBorderSum);
}

int Program01::Max3(int a, int b, int c)
{
	int max;
	if (a > b)
		max = a;
	else
		max = b;
	if (max > c)
		return max;
	else
		return c;
}