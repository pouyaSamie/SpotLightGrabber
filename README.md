# Windows 10 SpotLight Image Grabber
This project is a windows Service that you can config to copy all the lock screen images of windows 10 to custom path 

### Usage

1- Build The project

2- copy the exe and the config file to desire path

3- open cmd as Admin and run this command : sc.exe create SpotlightGrabber binPath= "path that you have copied the file\SpotlightImages.exe"

4- open windows services ( Control panel\Administrative Tools\Services) 

5- find SpotlightGrabber and right click on it and click start

that's it.


### Config 
in config file you can config some behaviours 


| Config        | Value                                                  |     Description     |
| ------------- |:------------------------------------------------------:| -------------------:|
| Interval      | milisecond                                             | default is 2 hourse |
| EnableLog     | Set to one to enable log the file                      |                     |
| LogPath       | the path that log file is giong to be create           |                     |
| DestinationPath | the path that you want the Image file to copy in that|                     |


