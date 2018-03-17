#pragma once
class Program02
{
public:
	Program02();
	~Program02();
	void Start();	
private:
	int len;
	int* arr;
	struct RetStruct
	{
		int res;
		int left;
		int right;
	};
	RetStruct* DivideAndConquer(int*, int, int);
	RetStruct* Max3(RetStruct*, RetStruct*, RetStruct*);
};



