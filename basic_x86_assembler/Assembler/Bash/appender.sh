rm -rf $3
cp bootloader.bin $3
dd if=/dev/zero of=$3 bs=1 count=1 seek=512
INITIAL_SIZE=$((($2-1) * 512 - 1))
dd if=/dev/zero of=$3 bs=1 count=1 seek=$INITIAL_SIZE
cat "$1" >> "$3"
dd if=/dev/zero of=$3 bs=1 count=1 seek=1474559
