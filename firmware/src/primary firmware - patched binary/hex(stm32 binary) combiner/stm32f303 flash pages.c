/******************************************************************************

                            Online C Compiler.
                Code, Compile, Run and Debug C program online.
Write your code in this editor and press "Run" button to compile and execute it.

*******************************************************************************/

#include <stdio.h>

#define K512 512*1024

int main()
{
    printf("Hello World\n");
    for (int i = 0; i < 256; i++) {
        printf("page %d:\t\t0x%x-0x%x\t\t%dK (0x%x)\n",i, i*2048, ((i+1)*2048-1), (K512-(i*2048))/1024, (K512-(i*2048)));
    }

    return 0;
}