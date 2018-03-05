# Windows 10 SpotLight Grabber
This project is a windows Service that you can config to copy all the lock screen images of windows 10 to custom path 

### Usage

1- Build The project

2- copy the exe and the config file to desire path

3- open cmd and go to the folder that you have the exe and config 

4- run this command sc install SpotlightImages.exe


### config 
in config file you can config some behaviours 


| Config        | Value                                                  |     Description     |
| ------------- |:------------------------------------------------------:| -------------------:|
| Interval      | milisecond                                             | default is 2 hourse |
| EnableLog     | Set to one to enable log the file                      |                     |
| LogPath       | the path that log file is giong to be create           |                     |
| DestinationPath | the path that you want the Image file to copy in that|                     |


