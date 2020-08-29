from flask import Flask, render_template, request
from werkzeug import secure_filename
import os
app = Flask(__name__)
app.config['UPLOAD_FOLDER'] = "./imageUploads"
print(app.config['UPLOAD_FOLDER'])

@app.route('/uploader', methods = ['GET', 'POST'])
def upload_file():
    if request.method == 'POST':
        print("POST")
        print(request.files)
        f = request.files['file']
        filename = secure_filename(f.filename)
        f.save(os.path.join(app.config['UPLOAD_FOLDER'], filename))
        return 'file uploaded successfully ' + f.filename

    elif request.method == 'GET':
        print("GET")
        return "GET invoked"

if __name__ == '__main__':
    app.run(host="0.0.0.0")
