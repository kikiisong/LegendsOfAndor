import os
import shutil as s
import datetime

def next_weekday(d, weekday):
    days_ahead = weekday - d.weekday()
    if days_ahead <= 0:
        days_ahead += 7
    return d + datetime.timedelta(days_ahead)

d = datetime.date(2011, 7, 2)
next_monday = next_weekday(d, 0)
name = "./" + next_monday.strftime("%Y-%m-%d") + ".md"

s.copy("./template.md", name)



    
