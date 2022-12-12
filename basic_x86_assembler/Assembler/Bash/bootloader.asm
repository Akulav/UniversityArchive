org 0x7C00

mov ah, 0
int 0x13 ; 


mov al, 3 		  
mov ch, 0          
mov dh, 0          
mov cl, 7d          
mov ah, 2 

mov bx, 0x7e00  
     
int 0x13   		  
jmp 0x7e00

times 510-($-$$) db 0
;Begin MBR Signature
db 0x55 ;byte 511 = 0x55
db 0xAA ;byte 512 = 0xAA
