import sys
import time
import logging
from watchdog.observers import Observer
from watchdog.events import FileSystemEventHandler
import socket

IP = "127.0.0.1"
Port = 8051

class eventHandler(FileSystemEventHandler):
    """Logs all the events captured."""

    def on_created(self, event):
        trimmedsrc = event.src_path[2:]
        sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
        sock.sendto(trimmedsrc, (IP, Port))
        sock.close()

        print "Sent " + trimmedsrc
        # logging.info("Created %s: %s", what, event.src_path)

if __name__ == "__main__":
    logging.basicConfig(
                        # filename = "log.txt", 
                        # filemode = "a",
                        level=logging.INFO,
                        format='%(asctime)s - %(message)s',
                        datefmt='%Y-%m-%d %H:%M:%S')
    path = sys.argv[1] if len(sys.argv) > 1 else '.'
    # event_handler = LoggingEventHandler()
    handler = eventHandler()
    observer = Observer()
    observer.schedule(handler, path, recursive=True)

    observer.start()
    try:
        while True:
            time.sleep(1)
    except KeyboardInterrupt:
        observer.stop()
    observer.join()
