#!/usr/bin/python3
import time
import os
from bs4 import BeautifulSoup as bs
import requests
import urllib.request as urlreq
import argparse

class InstaDownload():

    def __init__(self):
        parser = argparse.ArgumentParser("Download images from instagram")
        parser.add_argument("link", help="enter the link of the page where the image is")
        argument = parser.parse_args()
        if len(argument.link) > 0:
            self.downloader(argument.link)
        else:
            print("the link must be invalide")

    def downloader(self, link):

        if len(link) != 0:
            req =  requests.get(link)
            page_content = req.content

            html = bs(page_content, "html.parser")
            img = html.find("meta", attrs={"property" : "og:image"})
            image_link = img["content"]
            if "?" in image_link:
                url = image_link.split("?")
            url = url[0]
            print(url) # this print allows the c# program to see the url in the cmd output
            image_name = html.find("meta", attrs={"property" : "og:title"})
            filename = str(time.time())+".jpg"
            urlreq.urlretrieve(url, filename)


InstaDownload() 