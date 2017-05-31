

    org 0x3ff
    goto        main
    org 0x0
    goto    main
    
    
    
main:
     movai   0xaa
     movra   0x10
     movai   0xff
     movai   0x10
     movra   0x11
     addar   0x11
     addra   0x22
     adcar   0x11
     adcra   0x22
     rsubar  0x11
     rsubra  0x22
     rsbcar  0x11
     rsbcra  0x22
     daa
     dsa
     
     GOTO    main
     
     end