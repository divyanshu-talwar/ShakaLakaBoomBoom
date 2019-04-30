rm imageUploads/*.png
rm tempObj/*.obj
# rm objDownloads/*.obj
killall python

cd objDownloads
python -m SimpleHTTPServer &

cd ../
python ./upload.py & python objDownloads/objWatch.py & python imageUploads/imageWatch.py &
