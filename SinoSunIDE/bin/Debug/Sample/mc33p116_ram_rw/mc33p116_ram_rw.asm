#include mc33p116.inc

end_addr    equ   0x00
goal_data   equ   0x01
rw_data     equ   0x02
rw_addr     equ   0x03

rw_addr     equ   0x03



        org     0x00
        goto    start
        org 0x08
inte:	
        NOP 
 	goto    inte      
       
start:
       fill    0x00,0x108
       call    io_init
loop:
       movai   0x04
       movra   rw_addr
       movai   0xff
       movra   end_addr  
       movai   0x55
       movra   rw_data  
       call    ram_write0
       movai   0x55
       movra   goal_data
       movai   0x04
       movra   rw_addr
       movai   0xff
       movra   end_addr  
       call    ram_read0 

       movai   0x04
       movra   rw_addr
       movai   0xff
       movra   end_addr  
       movai   0xaa
       movra   rw_data  
       call    ram_write0
       movai   0xaa
       movra   goal_data
       movai   0x04
       movra   rw_addr
       movai   0xff
       movra   end_addr  
       call    ram_read0 
;;;;;;;;;;;;;;;;;;;;;;;;;;;       
       movai   0x00
       movra   rw_addr
       movai   0x1d
       movra   end_addr  
       movai   0x15
       movra   rw_data  
       call    ram_write1
       movai   0x15
       movra   goal_data
       movai   0x00
       movra   rw_addr
       movai   0x1d
       movra   end_addr  
       call    ram_read1 

       movai   0x00
       movra   rw_addr
       movai   0x1d
       movra   end_addr  
       movai   0x1a
       movra   rw_data  
       call    ram_write1
       movai   0x1a
       movra   goal_data
       movai   0x00
       movra   rw_addr
       movai   0x1d
       movra   end_addr  
       call    ram_read1 
;;;;;;;;;;;;;;;;;;;;;;;;;;; 
       movai   0x00
       movra   rw_addr
       movai   0xff
       movra   end_addr  
       movai   0x55
       movra   rw_data  
       call    ram_write2
       movai   0x55
       movra   goal_data
       movai   0x00
       movra   rw_addr
       movai   0xff
       movra   end_addr  
       call    ram_read2 

       movai   0x00
       movra   rw_addr
       movai   0xff
       movra   end_addr  
       movai   0xaa
       movra   rw_data  
       call    ram_write2
       movai   0xaa
       movra   goal_data
       movai   0x00
       movra   rw_addr
       movai   0xff
       movra   end_addr  
       call    ram_read2 
;;;;;;;;;;;;;;;;;;;;;;;;;;; 
     
       
ok_loop:   
       bset    IOP1,4
       NOP
       nop
       bclr    IOP1,4
       goto    ok_loop

io_init:       
       movai   0xff
       movra   IOP1
       movra   PUP1
       movra   OEP1 
       return
       
ram_write0: 
       movar   rw_addr
       rsubar  end_addr
       jbclr   Z
       return
       movar   rw_addr
       movra   FSR0
       movar   rw_data
       movra   INDF0 
       incr    rw_addr
       goto    ram_write0       

ram_write1: 
       movar   rw_addr
       rsubar  end_addr
       jbclr   Z
       return
       movar   rw_addr
       movra   FSR1
       movar   rw_data
       movra   INDF1 
       incr    rw_addr
       goto    ram_write1 
       
ram_write2: 
       movar   rw_addr
       rsubar  end_addr
       jbclr   Z
       return
       movar   rw_addr
       movra   FSR0
       movai   0x02
       movra   FSR1
       movar   rw_data
       movra   INDF2 
       incr    rw_addr
       goto    ram_write2        
       
ram_read0:
       movar   rw_addr
       rsubar  end_addr
       jbclr   Z
       return
       movar   rw_addr
       movra   FSR0
       movar   INDF0 
       rsubar  goal_data
       jbset   Z
       goto    data_error
       incr    rw_addr
       goto    ram_read0 

ram_read1:
       movar   rw_addr
       rsubar  end_addr
       jbclr   Z
       return
       movar   rw_addr
       movra   FSR1
       movar   INDF1 
       rsubar  goal_data
       jbset   Z
       goto    data_error
       incr    rw_addr
       goto    ram_read1 

ram_read2:
       movar   rw_addr
       rsubar  end_addr
       jbclr   Z
       return
       movar   rw_addr
       movra   FSR0
       movai   0x02
       movra   FSR1
       movar   INDF2 
       rsubar  goal_data
       jbset   Z
       goto    data_error
       incr    rw_addr
       goto    ram_read2 
       
data_error:
       bset    IOP1,5
       NOP
       nop
       bclr    IOP1,5
       goto    data_error


       org 0x0ffe
inte1:  	
        NOP 
	goto    inte1          
       end

        
