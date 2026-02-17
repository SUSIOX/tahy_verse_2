import React from 'react';
import { motion, AnimatePresence } from 'framer-motion';

interface Props {
    logs: string[];
    className?: string;
}

const LogPanel: React.FC<Props> = ({ logs, className = '' }) => {
    return (
        <div className={`flex-1 overflow-y-auto custom-scrollbar bg-black/40 rounded-xl border border-zinc-800 p-2 space-y-1 ${className}`}>
            <AnimatePresence initial={false}>
                {logs.length === 0 ? (
                    <div className="text-zinc-600 text-[11px] p-4 text-center italic">
                        Žádné záznamy k zobrazení
                    </div>
                ) : (
                    logs.map((log, i) => (
                        <motion.div
                            key={logs.length - i}
                            initial={{ opacity: 0, x: -10 }}
                            animate={{ opacity: 1, x: 0 }}
                            transition={{ duration: 0.2 }}
                            className="px-3 py-1.5 rounded bg-zinc-900/40 border border-zinc-800/30 text-[11px] text-zinc-400 font-mono"
                        >
                            <span className="text-zinc-600 mr-2">[{new Date().toLocaleTimeString()}]</span>
                            {log}
                        </motion.div>
                    ))
                )}
            </AnimatePresence>
        </div>
    );
};

export default LogPanel;
