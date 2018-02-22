/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfDay : MonoBehaviour
{

    public Light sun;
    public GameObject sunobject;
    public Light moon;
    public GameObject moonobject;
    public float rateoftime;
    public bool set_moon_position;
    public GameObject stars;
    public GameObject clouds;
    [Tooltip("Whether the moon should automatically be placed opposite to the sun")]
    private float orig;
    private float origdeg;
    private int day;
    private Vector3 lastFwd;
    private static TimeOfDay tod;
    private Vector3 sun_position;
    private Vector3 moon_position;

    // Use this for initialization
    void Start()
    {
        tod = this;
        if (sun == null || moon == null)
        {
            Debug.LogError("You have not set a light (sun) or moon object.");
            this.enabled = false;
        }
        else
        {
            if (set_moon_position)
            {
                moonobject.transform.SetPositionAndRotation(sunobject.transform.position, sunobject.transform.rotation);
                moonobject.transform.SetPositionAndRotation(new Vector3(sunobject.transform.position.x * -1, sunobject.transform.position.y * -1, sunobject.transform.position.z * -1), sunobject.transform.rotation);
                stars.transform.SetPositionAndRotation(moonobject.transform.position, stars.transform.rotation);
            }
            day = 0;
            lastFwd = sun.transform.forward;
            origdeg = Vector3.Angle(sun.transform.forward, lastFwd);
            sun_position = sunobject.transform.position;
            moon_position = moonobject.transform.position;
            Debug.Log("Current day: " + day);
        }
    }
    void Update()
    {
        orig = Vector3.Angle(sun.transform.forward, lastFwd);

        //Sun
        sun.transform.RotateAround(Vector3.zero, Vector3.right, rateoftime * Time.deltaTime);
        sunobject.transform.RotateAround(Vector3.zero, Vector3.right, rateoftime * Time.deltaTime);

        //Moon
        moon.transform.RotateAround(Vector3.zero, Vector3.right, rateoftime * Time.deltaTime);
        moonobject.transform.RotateAround(Vector3.zero, Vector3.right, rateoftime * Time.deltaTime);

        //Stars
        stars.transform.RotateAround(Vector3.zero, Vector3.right, rateoftime * Time.deltaTime);

        //Clouds
        clouds.transform.RotateAround(Vector3.zero, Vector3.up, (rateoftime / 12) * Time.deltaTime);

        if (orig == origdeg)
        {
            day += 1;
            Debug.Log("Current day: " + day);
        }
    }
    public static TimeOfDay getTimeOfDay()
    {
        return tod;
    }
    public void setRateOfTime(float rot)
    {
        rateoftime = rot;
    }
    public float getRateOfTime()
    {
        return rateoftime;
    }
    public int getDay()
    {
        return day;
    }
}
