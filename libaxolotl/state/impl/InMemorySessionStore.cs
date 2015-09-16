﻿using System;
using System.Collections.Generic;

namespace libaxolotl.state.impl
{
    public class InMemorySessionStore : SessionStore
    {
        static object Lock = new object();

        private IDictionary<AxolotlAddress, byte[]> sessions = new Dictionary<AxolotlAddress, byte[]>();

        public InMemorySessionStore() { }

        //[MethodImpl(MethodImplOptions.Synchronized)]
        public SessionRecord loadSession(AxolotlAddress remoteAddress)
        {
            try
            {
                if (containsSession(remoteAddress))
                {
                    byte[] session;
                    sessions.TryGetValue(remoteAddress, out session); // get()

                    return new SessionRecord(session);
                }
                else
                {
                    return new SessionRecord();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<uint> getSubDeviceSessions(String name)
        {
            List<uint> deviceIds = new List<uint>();

            foreach (AxolotlAddress key in sessions.Keys) //keySet()
            {
                if (key.getName().Equals(name) &&
                    key.getDeviceId() != 1)
                {
                    deviceIds.Add(key.getDeviceId());
                }
            }

            return deviceIds;
        }

        public void storeSession(AxolotlAddress address, SessionRecord record)
        {
            if (sessions.ContainsKey(address)) //mimic HashMap update behaviour
            {
                sessions.Remove(address);
            }
            sessions.Add(address, record.serialize()); // put
        }

        public bool containsSession(AxolotlAddress address)
        {
            return sessions.ContainsKey(address);
        }

        public void deleteSession(AxolotlAddress address)
        {
            sessions.Remove(address);
        }

        public void deleteAllSessions(String name)
        {
            foreach (AxolotlAddress key in sessions.Keys) // keySet()
            {
                if (key.getName().Equals(name))
                {
                    sessions.Remove(key);
                }
            }
        }
    }
}
