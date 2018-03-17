#pragma once
class Program01
{
public:
	Program01();
	~Program01();
	void Start();
private:
	int DivideAndConquer(int*, int, int);
	int Max3(int, int, int);
	int len;
	int* arr;
};

